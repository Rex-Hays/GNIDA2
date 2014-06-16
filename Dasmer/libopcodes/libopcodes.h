// Приведенный ниже блок ifdef - это стандартный метод создания макросов, упрощающий процедуру 
// экспорта из библиотек DLL. Все файлы данной DLL скомпилированы с использованием символа LIBOPCODES_EXPORTS,
// в командной строке. Этот символ не должен быть определен в каком-либо проекте
// использующем данную DLL. Благодаря этому любой другой проект, чьи исходные файлы включают данный файл, видит 
// функции LIBOPCODES_API как импортированные из DLL, тогда как данная DLL видит символы,
// определяемые данным макросом, как экспортированные.
#ifdef LIBOPCODES_EXPORTS
#define LIBOPCODES_API __declspec(dllexport)
#else
#define LIBOPCODES_API __declspec(dllimport)
#endif

// Этот класс экспортирован из libopcodes.dll
class LIBOPCODES_API Clibopcodes {
public:
	Clibopcodes(void);
	// TODO: Добавьте здесь свои методы.
};

extern LIBOPCODES_API int nlibopcodes;

LIBOPCODES_API int fnlibopcodes(void);
