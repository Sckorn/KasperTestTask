#include <iostream>

#include <boost/program_options.hpp>
#include <curl/curl.h>

#include "DirectoryHandler.h"

int main(int argc, char * argv[])
{
	namespace po = boost::program_options;
	po::options_description desc("Allowed options");
	desc.add_options()
		("help,h", "produce help message")
		("directory,d", po::value<std::string>(), "directory which will be scanned for files")
		("file,f", po::value<std::string>(), "specific file to, data of which will be sent to server");

	po::variables_map vm;
	po::store(po::parse_command_line(argc, argv, desc), vm);
	po::notify(vm);

	if (!vm.count("directory") && !vm.count("file"))
	{
		std::cerr << "Insufficient argument data!" << std::endl;
		return 1;
	}

	try
	{
		auto file_data_list = kasper_test_task_client::scanDirectory(vm["directory"].as<std::string>());
		if (!file_data_list.size())
			throw std::runtime_error("Supplied directory is empty!");

		CURL *curl;
		CURLcode res;

		if (curl_global_init(CURL_GLOBAL_ALL))
			throw std::runtime_error("Can't initialize curl!");

		curl = curl_easy_init();
		if (curl) {

			for (const auto &file_data : file_data_list)
			{
				/* Now specify the POST data */
				std::stringstream ss;
				ss << "http://localhost:10000/FileDataService/json/WriteFileDataToDb";
				ss << "?basename=" << file_data.basename << "&filepath=" << file_data.full_name
					<< "&filesize=" << file_data.size;

				auto str = ss.str();
				curl_easy_setopt(curl, CURLOPT_URL, str.c_str());
				/* example.com is redirected, so we tell libcurl to follow redirection */
				curl_easy_setopt(curl, CURLOPT_FOLLOWLOCATION, 1L);

				/* Perform the request, res will get the return code */
				res = curl_easy_perform(curl);
				/* Check for errors */
				if (res != CURLE_OK)
					std::cerr << "curl_easy_perform() failed: " << curl_easy_strerror(res) << std::endl;
			}
			/* always cleanup */
			curl_easy_cleanup(curl);
		}
		else
		{
			throw std::runtime_error("Can't get curl handle!");
		}	

		curl_global_cleanup();
	}
	catch (const std::exception &e)
	{
		std::cerr << "Exception in main routine: " << e.what() << std::endl;
		return 1;
	}

	return 0;
}