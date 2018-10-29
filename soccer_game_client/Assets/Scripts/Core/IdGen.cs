namespace Ssg.Core
{
    public class IdGen
    {
        private int m_nextId = 1;

        public int GetId()
        {
            int id = m_nextId;
            m_nextId++;
            return id;
        }
    }
}
