// libbfd.cpp: ���������� ���������������� ������� ��� ���������� DLL.
//

#include "stdafx.h"
#include "libbfd.h"
#include "sysdep.h"
#include "bfd.h"

// ������ ���������������� ����������
LIBBFD_API int nlibbfd=0;
LIBBFD_API bfd *bfd_open(const char *filename, const char *target)
{
	return bfd_openr(filename, target);
}

// ������ ���������������� �������.
LIBBFD_API int fnlibbfd(void)
{
	return 42;
}

// ����������� ��� ����������������� ������.
// ��. ����������� ������ � libbfd.h
Clibbfd::Clibbfd()
{
	return;
}
