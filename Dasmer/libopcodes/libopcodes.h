// ����������� ���� ���� ifdef - ��� ����������� ����� �������� ��������, ���������� ��������� 
// �������� �� ��������� DLL. ��� ����� ������ DLL �������������� � �������������� ������� LIBOPCODES_EXPORTS,
// � ��������� ������. ���� ������ �� ������ ���� ��������� � �����-���� �������
// ������������ ������ DLL. ��������� ����� ����� ������ ������, ��� �������� ����� �������� ������ ����, ����� 
// ������� LIBOPCODES_API ��� ��������������� �� DLL, ����� ��� ������ DLL ����� �������,
// ������������ ������ ��������, ��� ����������������.
#ifdef LIBOPCODES_EXPORTS
#define LIBOPCODES_API __declspec(dllexport)
#else
#define LIBOPCODES_API __declspec(dllimport)
#endif

// ���� ����� ������������� �� libopcodes.dll
class LIBOPCODES_API Clibopcodes {
public:
	Clibopcodes(void);
	// TODO: �������� ����� ���� ������.
};

extern LIBOPCODES_API int nlibopcodes;

LIBOPCODES_API int fnlibopcodes(void);
