using MoodStateApi.Models;

namespace MoodStateApi.Services {

    public interface IService {
        StateModel Get();
        StateModel Update(States state);
    }
}