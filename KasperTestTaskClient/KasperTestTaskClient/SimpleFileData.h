#ifndef SIMPLE_FILE_DATA_H
#define SIMPLE_FILE_DATA_H

#include <string>

namespace kasper_test_task_client
{
	struct SimpleFileData
	{
		std::string basename;
		std::string full_name;
		size_t size;
	};
}

#endif
