#ifndef DIRECTORY_HANDLER_H
#define DIRECTORY_HANDLER_H

#include <string>

#include <boost/filesystem.hpp>

#include "SimpleFileData.h"

namespace kasper_test_task_client
{
std::vector<SimpleFileData> scanDirectory(const boost::filesystem::path &dir_path);
}

#endif
