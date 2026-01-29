using System;

public class SaveSystem {
    private ISaveService saveService;
    public SaveSystem(ISaveService save) {
        saveService = save;
    }

    public void Save(SaveData saveData) {
        saveService.Save(saveData);
    }

    public SaveData Load() {
        return saveService.Load();
    }
}

public interface ISaveService {
    void Save(SaveData data);
    SaveData Load();
}

[Serializable]
public class SaveData {
    public int Score;
}
