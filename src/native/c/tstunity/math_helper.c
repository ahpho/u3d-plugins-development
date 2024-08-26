#define TST_LIB

#include "math_helper.h"
#include "log_helper.h"
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
		LOGD("####=> [%s] dlopen %s with RTLD_NOLOAD success! rdoc_api=%p\n", __FUNCTION__, szModuleName, rdoc_api);
		return ret == 1;
	}
#endif
	LOGD("####=> [%s] dlopen %s with RTLD_NOLOAD failed!\n", __FUNCTION__, szModuleName);
	return false;
}
TST_API const char* RdwGetPathTemplate(void)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)
	return "n/a";
#else
	if (rdoc_api) {
		const char* ret = rdoc_api->GetCaptureFilePathTemplate();
		LOGD("####=> [%s] RdwGetPathTemplate success! template=%s\n", __FUNCTION__, ret);
		return ret;
	}
	else {
		LOGD("####=> [%s] RdwGetPathTemplate failed!\n", __FUNCTION__);
		return "n/a";
	}
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
TST_API void RdwStartCapture(void* devicePtr, void* wndHandle)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)
#else
	if (rdoc_api) {
		rdoc_api->StartFrameCapture(devicePtr, wndHandle);
		LOGD("####=> [%s] StartFrameCapture done. devicePtr=%p, wndHandle=%p\n", __FUNCTION__, devicePtr, wndHandle);
	}
	else {
		LOGD("####=> [%s] StartFrameCapture skipped, rdoc_api=NULL.\n", __FUNCTION__);
	}
#endif
}
TST_API void RdwEndCapture(void* devicePtr, void* wndHandle)
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)
#else
	if (rdoc_api) {
		rdoc_api->EndFrameCapture(devicePtr, wndHandle);
		LOGD("####=> [%s] EndFrameCapture done. devicePtr=%p, wndHandle=%p\n", __FUNCTION__, devicePtr, wndHandle);
	}
	else {
		LOGD("####=> [%s] EndFrameCapture skipped, rdoc_api=NULL.\n", __FUNCTION__);
	}
#endif
}
TST_API void RdwTriggerCapture()
{
#if defined(WIN32) || defined(_WIN32) || defined(WINDOWS)
#else
	if (rdoc_api) {
		rdoc_api->TriggerCapture();
		LOGD("####=> [%s] TriggerCapture done.\n", __FUNCTION__);
	}
	else {
		LOGD("####=> [%s] TriggerCapture skipped, rdoc_api=NULL.\n", __FUNCTION__);
	}
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
