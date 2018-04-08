#ifndef DIRECTORY_HANDLER_H
#define DIRECTORY_HANDLER_H

#include <string>

#include <boost/filesystem.hpp>

namespace kasper_test_task_client
{
struct SimpleFileData
{
	std::string basename;
	std::string full_name;
	size_t size;
};

std::vector<SimpleFileData> scanDirectory(const std::string &dir_path);
}

#endif
