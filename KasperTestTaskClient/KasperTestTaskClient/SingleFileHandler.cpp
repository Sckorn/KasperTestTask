#include "SingleFileHandler.h"

namespace kasper_test_task_client
{
SimpleFileData getFileData(const boost::filesystem::path &path)
{
	return { path.stem().string(), path.string(), path.size() };
}
}