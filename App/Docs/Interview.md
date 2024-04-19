# Simple Ecommerce .NET API

The Simple Ecommerce .NET API is a set of methods designed to manage products, brands, and families within an inventory system. Below are the details of the available methods in the API:

The implementation of this API will be done using the .NET or .NET Core framework, depending on the developer's preference. Development standards and best practices specific to the platform will be used to ensure the quality and security of the system.

## Product

### Add Product

This method allows adding a new product to the system. If an attempt is made to create a product that has been previously deleted, a new record will be created instead of reactivating the deletion. The modification date is recorded.

### Delete Product

It performs a logical deletion of a product in the products table, setting the deletion date.

### Update Product

Allows editing the description, cost price, selling price, as well as the associated brand and family of a product. The modification date is recorded.

### Listing Product

Returns a list of active products, sorted by modification date. It allows filtering by product code, brand id, or family id. At least one of the filters is mandatory when querying.

## Family and Brand

### Add Family and Brand

Allows adding a new family or brand to the system, recording the modification date.

### Delete Family and Brand

Performs a logical deletion of a family or brand, setting the corresponding deletion date. It does not allow deletion if there are associated active products (not deleted).

### Update Family and Brand

Allows modifying the description of an existing family or brand, recording the modification date.




