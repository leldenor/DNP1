using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using FamilyAPI.DataAccess;
using FamilyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FamilyAPI.Data.Impl
{
    public class FamilyService:IFamilyService
    {
        public IList<Family> Families { get; private set; }
        
        public async Task<IList<Family>> GetFamilies()
        {
            using (FamilyContext ctx=new FamilyContext())
            {
                Families=ctx.Families.Include(f=>f.Adults).Include(f=>f.Children).Include(f=>f.Pets).ToList();
                return Families;
            }
            
        }

        public async Task RemoveFamily(int HouseNumber, string StreetName)
        {
            using (FamilyContext ctx = new FamilyContext())
            {
                Family fam = await ctx.Families.FirstAsync(f =>
                    f.HouseNumber == HouseNumber && f.StreetName.Equals(StreetName));
                IList<Adult> adults = ctx.Families.Where(f =>
                    f.HouseNumber == HouseNumber && f.StreetName.Equals(StreetName)).SelectMany(a => a.Adults).ToList();
                foreach (var item in adults)
                {
                    ctx.Remove(item);
                }
                IList<Child> children = ctx.Families.Where(f =>
                    f.HouseNumber == HouseNumber && f.StreetName.Equals(StreetName)).SelectMany(a => a.Children).Include(c=>c.ChildInterests).ToList();
                foreach (var item in children)
                {
                    ctx.Remove(item);
                }
                IList<Pet> pets = ctx.Families.Where(f =>
                    f.HouseNumber == HouseNumber && f.StreetName.Equals(StreetName)).SelectMany(a => a.Pets).ToList();
                foreach (var item in pets)
                {
                    ctx.Remove(item);
                }
                ctx.Remove(fam);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task AddFamily(Family family)
        {
            using (FamilyContext ctx = new FamilyContext())
            {
                await ctx.Families.AddAsync(family);
                await ctx.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Family family)
        {
            using (FamilyContext ctx = new FamilyContext())
            {
                Family fam = Families.First(f =>
                    f.HouseNumber == family.HouseNumber && f.StreetName.Equals(family.StreetName));
                if (fam.Adults.Count < family.Adults.Count)
                {
                    AddAdult(family,fam,ctx);
                }else if (fam.Adults.Count > family.Adults.Count)
                {
                    RemoveAdult(family,fam,ctx);
                }else if (fam.Children.Count < family.Children.Count)
                {
                    AddChild(family,fam,ctx);
                }else if (fam.Children.Count > family.Children.Count)
                {
                    RemoveChild(family,fam,ctx);
                }else if (fam.Pets.Count < family.Pets.Count)
                {
                    AddPet(family,fam,ctx);
                }else if (fam.Pets.Count > family.Pets.Count)
                {
                    RemovePet(family,fam,ctx);
                }
                ctx.Update(fam);
                await ctx.SaveChangesAsync();
            }
        }

        private void AddAdult(Family family, Family fam, FamilyContext ctx)
        {
            foreach (var item in family.Adults.Where(item => item.Id == 0))
            {
                fam.Adults.Add(item);
            }
        }

        private void RemoveAdult(Family family, Family fam, FamilyContext ctx)
        {
            IList<Adult> ad = new List<Adult>(fam.Adults);
            foreach (var item in fam.Adults.Where(item => family.Adults.Any(i => i.Id==item.Id)))
            {
                ad.Remove(item);
            }
            foreach (var item in ad)
            {
                ctx.Remove(item);
            }
        }
        private void AddChild(Family family, Family fam, FamilyContext ctx)
        {
            
            foreach (var item in family.Children.Where(item => item.Id == 0))
            {
                fam.Children.Add(item);
            }
        }

        private void RemoveChild(Family family, Family fam, FamilyContext ctx)
        {
            IList<Child> ad = new List<Child>(fam.Children);
            foreach (var item in fam.Children.Where(item => family.Children.Any(i => i.Id==item.Id)))
            {
                ad.Remove(item);
            }
            foreach (var item in ad)
            {
                ctx.Remove(item);
            }
        }
        private void AddPet(Family family, Family fam, FamilyContext ctx)
        {
            foreach (var item in family.Pets.Where(item => item.Id == 0))
            {
                fam.Pets.Add(item);
            }
        }

        private void RemovePet(Family family, Family fam, FamilyContext ctx)
        {
            IList<Pet> ad = new List<Pet>(fam.Pets);
            foreach (var item in fam.Pets.Where(item => family.Pets.Any(i => i.Id==item.Id)))
            {
                ad.Remove(item);
            }
            foreach (var item in ad)
            {
                ctx.Remove(item);
            }
        }
    }
    }