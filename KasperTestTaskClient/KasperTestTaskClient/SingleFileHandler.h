#ifndef SINGLE_FILE_HANDLER_H
#define SINGLE_FILE_HANDLER_H

#include <boost/filesystem.hpp>

#include "SimpleFileData.h"

namespace kasper_test_task_client
{
SimpleFileData getFileData(const boost::filesystem::path &path);
}

#endif
