select *
from product p, ProductItem pi, Item i, Section s
where p.Id= 4
	and pi.ProductId = p.Id
	and i.Id = pi.ItemId
	and s.Id = i.SectionId




(from p in db.Products
                                  join pi in db.ProductItems on p.Id equals pi.ProductId
                                  join i in db.Items on pi.ItemId equals i.Id
                                  join s in db.Sections on i.SectionId equals s.Id
                                  where p.Id.Equals(productId)