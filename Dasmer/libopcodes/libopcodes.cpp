// libopcodes.cpp: определяет экспортированные функции для приложения DLL.
//

#include "stdafx.h"
#include "libopcodes.h"
#include "dis-asm.h"
#include "bfd.h"


// Пример экспортированной переменной
LIBOPCODES_API int nlibopcodes=0;

disassemble_info dinfo = { 0 };
int custom_fprintf(void * stream, const char * format, ...)
{
	return 0;
}
// Пример экспортированной функции.
LIBOPCODES_API disassembler_ftype init_disassembler(bfd *Bfd)
{
	init_disassemble_info(&dinfo, NULL, custom_fprintf);
	dinfo.flavour = bfd_get_flavour(Bfd);
	dinfo.arch = bfd_get_arch(Bfd);
	dinfo.mach = bfd_get_mach(Bfd);
	dinfo.endian = Bfd->xvec->byteorder;
	disassemble_init_for_target(&dinfo);
	return disassembler(Bfd);
}

LIBOPCODES_API int disasm(bfd_vma vma, disassembler_ftype disassembler, disassemble_info dinfo)
{
	return disassembler(vma, &dinfo);
}
// Конструктор для экспортированного класса.
// см. определение класса в libopcodes.h
Clibopcodes::Clibopcodes()
{
	return;
}
