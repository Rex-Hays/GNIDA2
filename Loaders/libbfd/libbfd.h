// ����������� ���� ���� ifdef - ��� ����������� ����� �������� ��������, ���������� ��������� 
// �������� �� ��������� DLL. ��� ����� ������ DLL �������������� � �������������� ������� LIBBFD_EXPORTS,
// � ��������� ������. ���� ������ �� ������ ���� ��������� � �����-���� �������
// ������������ ������ DLL. ��������� ����� ����� ������ ������, ��� �������� ����� �������� ������ ����, ����� 
// ������� LIBBFD_API ��� ��������������� �� DLL, ����� ��� ������ DLL ����� �������,
// ������������ ������ ��������, ��� ����������������.
#ifdef LIBBFD_EXPORTS
#define LIBBFD_API __declspec(dllexport)
#else
#define LIBBFD_API __declspec(dllimport)
#endif

// ���� ����� ������������� �� libbfd.dll
class LIBBFD_API Clibbfd {
public:
	Clibbfd(void);
	// TODO: �������� ����� ���� ������.
};

extern LIBBFD_API int nlibbfd;

LIBBFD_API int fnlibbfd(void);
