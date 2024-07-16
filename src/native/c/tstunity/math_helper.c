#define TST_LIB

#include "math_helper.h"
#include "renderdoc_app.h"

RENDERDOC_API_1_6_0* rdoc_api = NULL;

TST_API bool RdwInitModule(const char* szModuleName) // Android上请传入"libVkLayer_GLES_RenderDoc.so"
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)

#else
	// At init, on linux/android.
	// For android replace librenderdoc.so with libVkLayer_GLES_RenderDoc.so
	void* mod = dlopen(szModuleName, RTLD_NOW | RTLD_NOLOAD);
	if (mod)
	{
		pRENDERDOC_GetAPI RENDERDOC_GetAPI = (pRENDERDOC_GetAPI)dlsym(mod, "RENDERDOC_GetAPI");
		int ret = RENDERDOC_GetAPI(eRENDERDOC_API_Version_1_6_0, (void **)&rdoc_api);
		return ret == 1;
	}
#endif
	return false;
}
TST_API const char* RdwGetPathTemplate(void)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)
	return "n/a";
#else
	return rdoc_api ? rdoc_api->GetCaptureFilePathTemplate() : "n/a";
#endif
}
TST_API void RdwSetPathTemplate(const char* szTemplate)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)

#else
	if (rdoc_api)
		rdoc_api->SetCaptureFilePathTemplate(szTemplate);
#endif
}
TST_API void RdwStartCapture(size_t devicePtr, size_t wndHandle)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)

#else
	if (rdoc_api)
		rdoc_api->StartFrameCapture(devicePtr, wndHandle);
#endif
}
TST_API void RdwEndCapture(size_t devicePtr, size_t wndHandle)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)

#else
	if (rdoc_api)
		rdoc_api->EndFrameCapture(devicePtr, wndHandle);
#endif
}



TST_API int tst_add(int a, int b){
	return a + b;
}
TST_API int tst_sub(int a, int b){
	return a - b;
}
TST_API int tst_div(int a, int b){
	return a / b;
}
TST_API int tst_mul(int a, int b){
	return a * b;
}
