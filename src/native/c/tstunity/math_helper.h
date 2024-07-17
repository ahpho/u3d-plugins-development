#ifndef MATH_HELPER_
#define MATH_HELPER_

#include <stddef.h>         // printf
#include <stdbool.h>        // bool
#include <dlfcn.h>          // dlopen
#include "tstunity_conf.h"

TST_API int tst_add(int a, int b);
TST_API int tst_sub(int a, int b);
TST_API int tst_div(int a, int b);
TST_API int tst_mul(int a, int b);

TST_API bool RdwInitModule(const char* szModuleName);
TST_API const char* RdwGetPathTemplate(void);
TST_API void RdwSetPathTemplate(const char* szTemplate);
TST_API void RdwStartCapture(void* devicePtr, void* wndHandle);
TST_API void RdwEndCapture(void* devicePtr, void* wndHandle);

#endif//MATH_HELPER_
