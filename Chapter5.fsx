type Undefined = exn

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

type CustomerInfo = Undefined  // like the *hole* in Idris
type ShippingAddress = Undefined
type BillingAddress = Undefined
type OrderLine = Undefined
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
