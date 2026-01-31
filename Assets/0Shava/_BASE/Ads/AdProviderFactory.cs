public static class AdProviderFactory {
    public static IAdProvider Create() {
#if PLUGIN_YG_2
        return new YG2AdProvider();
#endif
    }
}