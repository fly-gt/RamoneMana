public class AEShared : Singletone<AEShared> {
    public AudioLibraryAsset libraryAsset;

    public static AudioLibraryAsset asset => Instance == null ? null : Instance.libraryAsset;
}
