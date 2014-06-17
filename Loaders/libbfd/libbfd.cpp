// libbfd.cpp: ���������� ���������������� ������� ��� ���������� DLL.
//

#include "stdafx.h"
#include "libbfd.h"
#include "sysdep.h"
#include "bfd.h"

// ������ ���������������� ����������
LIBBFD_API int nlibbfd=0;

bfd* Bfd;

extern "C" __declspec(dllexport) bfd* __stdcall bfd_open(const char *filename, const char *target)
{
	bfd_init();
	Bfd = bfd_openr(filename, target);
	return Bfd;
}
extern "C" __declspec(dllexport) bool __stdcall bfd_check(const char *filename, const char *target)
{
	bfd* Bfd1;
	bfd_init();
	Bfd1 = bfd_openr(filename, target);
	bool res = bfd_check_format(Bfd1, bfd_object);
	bfd_close(Bfd1);
	return res;

}

extern "C" __declspec(dllexport) ULONG __stdcall bfd_entrypoint(bfd* BFD)
{
	return bfd_get_start_address(BFD);
}

extern "C" __declspec(dllexport) int __stdcall bfd_sec_count(bfd* BFD)
{
	return bfd_count_sections(BFD);
}

// ������ ���������������� �������.
extern "C" __declspec(dllexport) int __stdcall fnlibbfd(void)
{
	return 42;
}

// ����������� ��� ����������������� ������.
// ��. ����������� ������ � libbfd.h
Clibbfd::Clibbfd()
{
	return;
}
