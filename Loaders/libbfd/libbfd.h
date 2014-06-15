// Приведенный ниже блок ifdef - это стандартный метод создания макросов, упрощающий процедуру 
// экспорта из библиотек DLL. Все файлы данной DLL скомпилированы с использованием символа LIBBFD_EXPORTS,
// в командной строке. Этот символ не должен быть определен в каком-либо проекте
// использующем данную DLL. Благодаря этому любой другой проект, чьи исходные файлы включают данный файл, видит 
// функции LIBBFD_API как импортированные из DLL, тогда как данная DLL видит символы,
// определяемые данным макросом, как экспортированные.
#ifdef LIBBFD_EXPORTS
#define LIBBFD_API __declspec(dllexport)
#else
#define LIBBFD_API __declspec(dllimport)
#endif

// Этот класс экспортирован из libbfd.dll
class LIBBFD_API Clibbfd {
public:
	Clibbfd(void);
	// TODO: Добавьте здесь свои методы.
};

extern LIBBFD_API int nlibbfd;

LIBBFD_API int fnlibbfd(void);
