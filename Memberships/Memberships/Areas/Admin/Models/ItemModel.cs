using Memberships.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memberships.Areas.Admin.Models
{
    public class ItemModel
    {        
        public int Id { get; set; }
        [MaxLength(255)]
        [Required]
        public string Title { get; set; }
        [MaxLength(2048)]
        public string Description { get; set; }
        [MaxLength(1024)]
        [DisplayName("Url")]
        public string Url { get; set; }
        [MaxLength(1024)]
        [DisplayName("Image Url")]
        public string ImageUrl { get; set; }
        public string HTML { get; set; }
        [DefaultValue(0)]
        [DisplayName("Wait Days")]
        public int WaitDays { get; set; }
        public string HTMLShort { get { return HTML == null || HTML.Length < 50 ? HTML : HTML.Substring(0, 50); } }
        public int ItemTypeId { get; set; }
        public int SectionId { get; set; }
        public int PartId { get; set; }
        [DisplayName("Is Free")]
        public bool IsFree { get; set; }
        [DisplayName("Item Type")]
        public ICollection<ItemType> ItemTypes { get; set; }
        public string ItemType
        {
            get
            {
                return ItemTypes == null || ItemTypes.Count.Equals(0) ? string.Empty : ItemTypes.First(pt => pt.Id.Equals(ItemTypeId)).Title;
            }
        }
        [DisplayName("Chapter")]
        public ICollection<Section> Sections { get; set; }
        public string Section
        {
            get
            {
                return Sections == null || Sections.Count.Equals(0) ? string.Empty : Sections.First(pt => pt.Id.Equals(SectionId)).Title;
            }
        }
        [DisplayName("Part")]
        public ICollection<Part> Parts { get; set; }
        public string Part
        {
            get
            {
                return Parts == null || Parts.Count.Equals(0) ? string.Empty : Parts.First(pt => pt.Id.Equals(PartId)).Title;
            }
        }
    }
}