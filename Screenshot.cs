namespace BuildingGame;

public static class Screenshot
{
    public static readonly string ScreenshotRoot = "screenshots/";
    public static void Create()
    {
        // create root for screenshots
        if (!Directory.Exists(ScreenshotRoot)) Directory.CreateDirectory(ScreenshotRoot);

        // take screenshot
        TakeScreenshot(ScreenshotRoot + DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss_fff") + ".png");
    }
}