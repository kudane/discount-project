> Kraisorn Rittiwong (kraisorn204@gmail.com)

## โปรแกรมคำนวณส่วนลดสินค้าฟังก์ชันการทำงานดังนี้
1. อ่านข้อมูลส่วนลดจากไฟล์ JSON
2. คำนวณส่วนลดจากข้อมูลที่อ่านได้
3. แสดงผลลัพธ์ส่วนลดที่คำนวณได้

## เงื่อนไขการทำงาน(ตามที่เข้าใจ)
1. Coupon แบบ Amount จะคำนวณตามสัดส่วนของราคาสินค้าในตะกร้า
2. coupon แบบ Percentage จะแปลง % เป็น Amount แล้วจะคำนวณตามสัดส่วนของราคาสินค้าในตะกร้า
3. Ontop แบบ Point จะนำ point ไปคำนวณตามสัดส่วนของราคาสินค้าในตะกร้า โดยลดได้มากสุดที่ 20% ของราคาสินค้าล่าสุด
4. Ontop แบบ Percentage By Catagory จะนำ % ไปคำนวณตามรายการสินค้าแต่ละชิ้นในหมวดหมู่ที่ตรงกัน
5. Seasonal จะนำ % ไปคำนวณตามสัดส่วนของราคาสินค้าในตะกร้า


## การติดตั้งและรันโปรแกรม
### data.json
1. Items: รายการสินค้าที่มีราคาและหมวดหมู่
2. Campaigns: รายการส่วนลดที่สามารถใช้ได้ใส่ config ได้ดังนี้
หรือ สามารถดูได้จาก class ที่มีชื่อลงท้าย Spec 

```json
// Coupon แบบ Amount
{
  "Type": "CouponByAmount",
  "Spec": {
    "Amount": 100.0
  }
}

// Coupon แบบ Percent
{
  "Type": "CouponByAmount",
  "Spec": {
    "Amount": 100.0
  }
}

// Ontop แบบ Per Catagory
{
  "Type": "OntopByPercentOfCatagory",
  "Spec": {
    "Percent": 5.0,
    "ItemCategory": "Clothing"
  },
}

// Ontop แบบ Point
{
  "Type": "OntopByPoint",
  "Spec": {
    "Point": 100.0
  }
}

// Seasonal
{
  "Type": "Seasonal",
  "Spec": {
    "EveryPrice": 100.0,
    "Amount": 10.0
  }
}
```

      
3. Oders: ลำดับที่ต้องการคำนวณ Discount ใส่ config ได้ดังนี้
โดยนำค่าจาก CampaignEnum.cs มาใส่ใน key "Campaigns" แบบ array

```json
{
   "No": 1,
   "Campaigns": ["CouponByAmount", "CouponByPercent"]
},
{
   "No": 2,
   "Campaigns": ["OntopByPercentOfCatagory", "OntopByPoint"]
},
{
   "No": 3,
   "Campaigns": ["Seasonal"]
}
```

## Example data.json
```json
{
  "Items": [
    {
      "Name": "Hood",
      "Price": 100,
      "Catagory": "Clothing"
    },
    {
      "Name": "T-Shirt",
      "Price": 100,
      "Catagory": "Clothing"
    },
    {
      "Name": "Watch",
      "Price": 100,
      "Catagory": "Electronics"
    },
    {
      "Name": "Bag",
      "Price": 100,
      "Catagory": "Accessories"
    }
  ],
  "Campaigns": [
    {
      "Type": "CouponByAmount",
      "Spec": {
        "Amount": 100.0
      }
    },
    {
      "Type": "OntopByPercentOfCatagory",
      "Spec": {
        "Percent": 5.0,
        "ItemCategory": "Clothing"
      }
    },
    {
      "Type": "Seasonal",
      "Spec": {
        "EveryPrice": 100.0,
        "Amount": 10.0
      }
    }
  ],
  "Orders": [
    {
      "No": 1,
      "Campaigns": [ "CouponByAmount", "CouponByPercent" ]
    },
    {
      "No": 2,
      "Campaigns": [ "OntopByPercentOfCatagory", "OntopByPoint" ]
    },
    {
      "No": 3,
      "Campaigns": [ "Seasonal" ]
    }
  ]
}
```


### Project Stucture
```
- src
   |- DiscountApp
   |   |- data.json        // ไฟล์ข้อมูลส่วนลด (Discount Data)
   |   |- ...              // ไฟล์โค้ดหลักของแอปพลิเคชัน (UI, Program.cs, ฯลฯ)
   |
   |- DiscountModule
   |   |- ...              // ไลบรารีหรือคลาสสำหรับคำนวณส่วนลด (Discount Logic)

- test
   |- DiscountModule-Test
   |   |- ...              // โค้ดสำหรับ Unit Test ของ DiscountModule
```


![Local image](https://github.com/kudane/discount-project/blob/main/mermaid-diagram-2025-06-30-011337.png)