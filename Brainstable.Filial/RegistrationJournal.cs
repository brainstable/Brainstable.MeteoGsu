using System.Collections.Generic;

namespace Brainstable.Filial
{
    public abstract class RegistrationJournal<T> where T : class
    {
        Dictionary<int, T> registrationNotes = new Dictionary<int, T>();


        public Dictionary<int, T> RegistrationNotes
        {
            get => registrationNotes;
            set => registrationNotes = value;
        }
    }
}
    