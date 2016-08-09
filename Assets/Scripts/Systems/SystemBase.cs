using System.Collections;


namespace PacmanGame
{
	public class SystemBase
	{
        string name;
        public string Name
        {
            get { return name; }
        }
        public GameLevel level;

        public SystemBase(string _name)
        {
            name = _name;
        }
        public virtual void Create()
        {
        }
	}
}
