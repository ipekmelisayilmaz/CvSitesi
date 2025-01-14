﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcCv.Models.Entity;
namespace MvcCv.Repositories
{
    //GENERİC YAPI KURULDU...........
    public class GenericRepository<T> where T : class, new()
    {
        DbCvEntities db = new DbCvEntities();
        public List<T> List()//EF KOMUTLARINDAN LİSTELEME
        {
        return db.Set<T>().ToList();    // T değeri bütün tablolar olabilir
        }
        public void TAdd(T p)//EKLEME
        {
            db.Set<T>().Add(p);
             db.SaveChanges();
        }
        public void TDelete(T p)//SİLME
        {
            db.Set<T>().Remove(p);
            db.SaveChanges();
        }
        public T TGet(int id)//ID'ye göre getir
        {
            return db.Set<T>().Find(id);    
        }
        public void TUpdate(T p)
        {
            db.SaveChanges();
        }
    }
}