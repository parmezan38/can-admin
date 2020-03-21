using CANAdmin.Shared.Models;
using CANAdmin.Shared.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CANAdmin.Data
{
    public class CANDatabaseManager : ICANDatabaseManager
    {
        private readonly ApplicationDbContext _db;
        private readonly IDbcParser _dbcParser;

        public CANDatabaseManager(ApplicationDbContext db, IDbcParser dbcParser)
        {
            _db = db;
            _dbcParser = dbcParser;
        }

        public void Add(FileModel file)
        {
            CANDatabase canDb = _dbcParser.ParseFile(file);
            try
            {
                _db.CANDatabases.Add(canDb);
                _db.SaveChanges();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred.");
            }
        }

        public List<CANDatabaseView> Get()
        {
            try
            {
                List<CANDatabaseView> list = _db.CANDatabases
                .Select(c => new CANDatabaseView()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Date = c.Date,
                    NetworkNodes = c.NetworkNodes.Select(n => new NetworkNodeView() { Name = n.Name }).ToList(),
                    Messages = c.Messages.Select(m => new MessageView()
                    {
                        MessageId = m.MessageId,
                        Name = m.Name,
                        Signals = m.Signals.Select(s => new SignalView()
                        {
                            Name = s.Name,
                            StartBit = s.StartBit,
                            Length = s.Length
                        })
                        .ToList()
                    })
                    .ToList()
                })
                .OrderByDescending(c => c.Date)
                .ToList();
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Delete(int id)
        {
            try
            {
                _db.CANDatabases.Remove(new CANDatabase() { Id = id });
                _db.SaveChanges();
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
