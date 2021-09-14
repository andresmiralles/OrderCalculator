### Assumptions:
* Assuming another system or component already put the order together with the associated products, coupons, promotions, etc.
* Client Identifier is unique in the datasource will be used for the creation of the OrderItem calculators for specific behavior
    * Risk: Component creating the Order and the relationship needs to be aware of this convention
* The Order is coming from the Data Repositories with the object relationship
* Coupons in tax-before States are calculated from the Base Price but applied to the After Tax amount
* Promotions in tax-before States are applied to after-coupons discounted amount and based on: base price - coupons
    * Assuming how tax-before works
* Lower level environments and connections string assumed to be done
* Discount percentage will only be allowed to be bigger than 0 and smaller than 1
    * Risk: Not much, just in case this varies a lot per Client
* Just for testing purposes NY & NM luxury items taxed twice
    * Risk: None, just assuming for testing purposes