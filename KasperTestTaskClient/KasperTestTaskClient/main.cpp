#include <iostream>

#include <boost/program_options.hpp>
#include <curl/curl.h>

#include "DirectoryHandler.h"
#include "SingleFileHandler.h"

int main(int argc, char * argv[])
{
	namespace po = boost::program_options;
	po::options_description desc("Allowed options");
	desc.add_options()
		("help,h", "produce help message")
		("input,i", po::value<std::string>(), "directory which will be scanned for files")
		;

	po::variables_map vm;
	po::store(po::parse_command_line(argc, argv, desc), vm);
	po::notify(vm);

	if (!vm.count("input"))
	{
		std::cerr << "Insufficient argument data!" << std::endl;
		return 1;
	}

	try
	{
		boost::filesystem::path p(vm["input"].as<std::string>());
		std::vector<kasper_test_task_client::SimpleFileData> file_data_list;
		if (boost::filesystem::is_directory(p))
		{
			file_data_list = kasper_test_task_client::scanDirectory(p);
		}
		else
		{
			file_data_list.push_back(kasper_test_task_client::getFileData(p));
		}
		
		if (!file_data_list.size())
			throw std::runtime_error("No files found by input path!");

		CURL *curl;
		CURLcode res;

		if (curl_global_init(CURL_GLOBAL_ALL))
			throw std::runtime_error("Can't initialize curl!");

		curl = curl_easy_init();
		if (curl) {

			for (const auto &file_data : file_data_list)
			{
				std::stringstream ss;
				ss << "http://localhost:10000/FileDataService/json/WriteFileDataToDb";
				ss << "?basename=" << file_data.basename << "&filepath=" << file_data.full_name
					<< "&filesize=" << file_data.size;

				auto str = ss.str();
				curl_easy_setopt(curl, CURLOPT_URL, str.c_str());
				curl_easy_setopt(curl, CURLOPT_FOLLOWLOCATION, 1L);

				res = curl_easy_perform(curl);
				
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