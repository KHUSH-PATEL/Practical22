using DataAccessLayer.Data;
using DataAccessLayer.Services;

namespace DataAccessLayer.Singleton
{
    public sealed class SingletonClass
    {
        private SingletonClass()
        {            
        }
        private static AppDbContext _context = null;
        public static AppDbContext GetInstance()
        {
            if (_context == null)
            {
                _context = new AppDbContext();
            }
            return _context;
        }
        private static Logger _logger = null;
        public static Logger GetInstanceLogger()
        {
            if (_logger == null)
            {
                _logger = new Logger();
            }
            return _logger;
        }
    }
}
