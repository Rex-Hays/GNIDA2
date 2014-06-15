// libbfd.cpp: определяет экспортированные функции для приложения DLL.
//

#include "stdafx.h"
#include "libbfd.h"
#include "sysdep.h"
#include "bfd.h"

// Пример экспортированной переменной
LIBBFD_API int nlibbfd=0;
LIBBFD_API bfd *bfd_open(const char *filename, const char *target)
{
	return bfd_openr(filename, target);
}

// Пример экспортированной функции.
LIBBFD_API int fnlibbfd(void)
{
	return 42;
}

// Конструктор для экспортированного класса.
// см. определение класса в libbfd.h
Clibbfd::Clibbfd()
{
	return;
}
