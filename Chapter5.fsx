// 5.3: Modeling Simple Values

type CustomerId = CustomerId of int

type WidgetCode = WidgetCode of string // starting with "W" then 4 digits
type GizmoCode = GizmoCode of string   // starting with "G" then 3 digits

type UnitQuantity = UnitQuantity of int

type KilogramQuantity = KilogramQuantity of decimal

let customerId = CustomerId 42

let (CustomerId innerValue) = customerId

printfn "%i" innerValue

let processCustomerId (CustomerId inner) = printfn "inner value is %i" inner

processCustomerId customerId

type UnitQuantity2 = int  // type alias

[<Struct>]
type UnitQuantity3 = UnitQuantity3 of int

type UnitQuantities = UnitQuantities of int[]

// 5.4: Modeling Complex Data

type Undefined = exn

type CustomerInfo = Undefined  // like the *hole* in Idris
type ShippingAddress = Undefined
type BillingAddress = Undefined

type OrderId = OrderId of int
type ProductId = ProductId of int

[<NoEquality;NoComparison>]  // implemented in section 5.7
type OrderLine = {
  OrderId: OrderId
  ProductId : ProductId
  Qty: int
  }
  with
  member this.Key =
    (this.OrderId, this.ProductId)

type BillingAmount = Undefined

type Order = {
  CustomerInfo : CustomerInfo
  ShippingAddress : ShippingAddress
  BillingAddress : BillingAddress
  OrderLines : OrderLine list
  AmountToBill : BillingAmount
}

type ProductCode =
  | Widget of WidgetCode
  | Gizmo of GizmoCode

type OrderQuantity =
  | Unit of UnitQuantity
  | Kilogram of KilogramQuantity

// 5.5: Modeling Workflows with Functions

type UnvalidatedOrder = Undefined
type ValidatedOrder = Undefined
type ValidateOrder = UnvalidatedOrder -> ValidatedOrder

type AcknowledgementSent = Undefined
type OrderPlaced = Undefined
type BillableOrderPlaced = Undefined
type PlaceOrderEvents = {
  AcknowledgementSent : AcknowledgementSent
  OrderPlaced : OrderPlaced
  BillableOrderPlaced : BillableOrderPlaced
}

type PlaceOrder = UnvalidatedOrder -> PlaceOrderEvents

type QuoteForm = Undefined
type OrderForm = Undefined
type EnvelopeContents = Undefined
type CategoriedMail = Undefined

type CategorizedMail =
  | Quote of QuoteForm
  | Order of OrderForm

type CategorizeInboundMail = EnvelopeContents -> CategoriedMail

type ProductCatalog = Undefined
type PricedOrder = Undefined
type CalculatePrices = OrderForm -> ProductCatalog -> PricedOrder

type CalculatePricesInput = {
  OrderForm : OrderForm
  ProductCatalog : ProductCatalog
}

type CalculatePrices2 = CalculatePricesInput -> PricedOrder

// 5.5.x: Documenting Effects in the Function Signature

type ValidateOrderWithEffect = UnvalidatedOrder -> Result<ValidatedOrder, ValidationError list>

and ValidationError = {
  FieldName : string
  ErrorDescription : string
}

type ValidationResponse<'a> = Async<Result<'a, ValidationError list>>

type ValidateOrderAsync = UnvalidatedOrder -> ValidationResponse<ValidatedOrder>

// 5.6: A Question of Identity: Value Objects

let widgetCode1 = WidgetCode "W1234"
let widgetCode2 = WidgetCode "W1234"
printfn "Codes are equal? %b" (widgetCode1 = widgetCode2)

type Person = {FirstName: string; LastName: string}

let name1 = {FirstName="Alex"; LastName="Adams"}
let name2 = {FirstName="Alex"; LastName="Adams"}
printfn "Names are equal? %b" (name1 = name2)

type Address = {StreetAddress: string; City: string; Zip: string}

let address1 = {StreetAddress="123"; City="bj"; Zip="100037"}
let address2 = {StreetAddress="123"; City="bj"; Zip="100037"}
printfn "Addresses are equal? %b" (address1 = address2)  // structural equality

// 5.7: A Question of Identity: Entities

type PhoneNumber = PhoneNumber of string
type EmailAddress = EmailAddress of string
type ContactId = ContactId of int
type ContactRaw = {
  ContactId: ContactId
  PhoneNumber: PhoneNumber
  EmailAddress: EmailAddress
}

// 5.7.x: Implementing Equality for Entities

// this OOP style implementation is not recommended
[<CustomEquality; NoComparison>]
type ContactOOP = {
  ContactId: ContactId
  PhoneNumber: PhoneNumber
  EmailAddress: EmailAddress
  }
  with
  override this.Equals(obj) = 
    match obj with
    | :? ContactOOP as c -> this.ContactId = c.ContactId  // what does ':?' mean?
    | _ -> false
  override this.GetHashCode() =
    hash this.ContactId

let contactId = ContactId 1

let contact1 = {
  ContactId = contactId
  PhoneNumber = PhoneNumber "12345678"
  EmailAddress = EmailAddress "abc@def.com"
}

let contact2 = {
  ContactId = contactId
  PhoneNumber = PhoneNumber "56781234"
  EmailAddress = EmailAddress "xyz@def.com"
}

printfn "ContactOOP are equal? %b" (contact1 = contact2)

// FP style implementation

[<NoEquality; NoComparison>]
type Contact = {
  ContactId: ContactId
  PhoneNumber: PhoneNumber
  EmailAddress: EmailAddress
}

let contact3 = {
  ContactId = contactId
  PhoneNumber = PhoneNumber "12345678"
  EmailAddress = EmailAddress "abc@def.com"
}

let contact4 = {
  ContactId = contactId
  PhoneNumber = PhoneNumber "12345678"
  EmailAddress = EmailAddress "abc@def.com"
}

// printfn "%b" (contact3 = contact4)
// compile error: 
// The type 'Contact' does not support the 'equality' constraint because it has the 'NoEquality' attribute

printfn "Contacts are equal? %b" (contact3.ContactId = contact4.ContactId)

// definition of OrderLine is above

let order1 = {
  OrderId = OrderId 31
  ProductId = ProductId 24
  Qty = 123
}

let order2 = {
  OrderId = OrderId 31
  ProductId = ProductId 24
  Qty = 987
}

printfn "Orders are equal? %b" (order1.Key = order2.Key)

let order3 = {order2 with Qty = 23}

printfn "Quantity of order3 is: %d" order3.Qty

// 5.8: Aggregates
