using EFDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFDemo
{
    public class DemoDbContext : DbContext , IDisposable
    {
        public virtual IDbSet<TodoEntity> DbEntities { get; set; }
    }
}