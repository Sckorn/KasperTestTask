#include "DirectoryHandler.h"

namespace kasper_test_task_client
{
std::vector<SimpleFileData> scanDirectory(const std::string &dir_path)
{
	std::vector<SimpleFileData> ret;

	boost::filesystem::path p(dir_path);
	if (!boost::filesystem::exists(p) || !boost::filesystem::is_directory(p))
		throw std::runtime_error("Supplied path either doesn't exist or is not a directory!");

	boost::filesystem::directory_iterator di(p);
	boost::filesystem::directory_iterator end_di;
	while (di != end_di)
	{
		auto entry = *di;

		boost::filesystem::path entry_path(entry);
		if (boost::filesystem::is_regular_file(entry_path))
		{
			ret.push_back({
				entry_path.stem().string(),
				entry_path.string(),
				entry_path.size()
			});
		}
		
		++di;
	}

	return ret;
}
}