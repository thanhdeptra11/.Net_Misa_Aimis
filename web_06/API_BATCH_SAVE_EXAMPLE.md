/**
 * ============================================
 * API: Batch Save (Insert/Update/Delete)
 * ============================================
 * 
 * Endpoint: POST /api/v1/{entity}/save-batch
 * Content-Type: application/json
 * 
 * Mô tả: Lưu hàng loạt dữ liệu với các state khác nhau
 *        - Insert: Thêm mới
 *        - Update: Cập nhật
 *        - Delete: Xóa
 */

// ============================================
// EXAMPLE 1: Batch Save Customer (Mixed Operations)
// ============================================
POST /api/v1/customer/save-batch
Content-Type: application/json

[
  {
    "id": "00000000-0000-0000-0000-000000000000",
    "customerCode": "CUST001",
    "customerName": "Công ty A",
    "customerAddr": "Hà Nội",
    "createdDate": "2024-01-15T10:30:00Z",
    "createdBy": "admin",
    "modifiedDate": null,
    "modifiedBy": null,
    "state": 1
  },
  {
    "id": "550e8400-e29b-41d4-a716-446655440000",
    "customerCode": "CUST002",
    "customerName": "Công ty B - Updated",
    "customerAddr": "TP HCM",
    "createdDate": "2024-01-15T10:30:00Z",
    "createdBy": "admin",
    "modifiedDate": "2024-01-16T14:30:00Z",
    "modifiedBy": "editor",
    "state": 2
  },
  {
    "id": "660e8400-e29b-41d4-a716-446655440001",
    "customerCode": "CUST003",
    "customerName": "Công ty C",
    "customerAddr": "Đà Nẵng",
    "createdDate": "2024-01-15T10:30:00Z",
    "createdBy": "admin",
    "modifiedDate": null,
    "modifiedBy": null,
    "state": 3
  }
]

// Response Success (200)
{
  "message": "Batch save completed successfully",
  "totalAffected": 3,
  "totalProcessed": 3
}

// Response Error - Validation (400)
{
  "message": "CustomerCode is required"
}

// ============================================
// EXAMPLE 2: Batch Save SaleOrder
// ============================================
POST /api/v1/saleorder/save-batch
Content-Type: application/json

[
  {
    "id": "00000000-0000-0000-0000-000000000000",
    "customerId": "550e8400-e29b-41d4-a716-446655440000",
    "saleOrderNo": "SO001",
    "saleOrderDate": "2024-01-16T10:00:00Z",
    "totalAmount": 1000000,
    "createdDate": "2024-01-16T10:30:00Z",
    "createdBy": "admin",
    "modifiedDate": null,
    "modifiedBy": null,
    "state": 1
  }
]

// Response (200)
{
  "message": "Batch save completed successfully",
  "totalAffected": 1,
  "totalProcessed": 1
}

// ============================================
// State Mapping
// ============================================
state = 0 : None (không làm gì)
state = 1 : Insert (thêm mới)
state = 2 : Update (cập nhật)
state = 3 : Delete (xóa)

// ============================================
// Flow Execution
// ============================================
1. Nhận list entities với state khác nhau
2. Validate BeforeSave:
   - Kiểm tra [Required] attributes
   - Kiểm tra [Unique] attributes
   - Throw ValidationException nếu lỗi
3. Thực hiện:
   - INSERT: State == 1 → AddAsync()
   - UPDATE: State == 2 → UpdateAsync()
   - DELETE: State == 3 → DeleteAsync()
4. Trả về tổng số rows affected

// ============================================
// Error Handling
// ============================================
BadRequest (400):
  - Entities list null/empty
  - ValidationException (required, unique, business rules)

InternalServerError (500):
  - Database errors
  - Unexpected exceptions

// ============================================
// Business Rules
// ============================================
1. [Required] fields phải có giá trị
2. [Unique] fields không được trùng (trừ NULL)
3. Add: Auto-generate ID
4. Update: Auto-set ModifiedDate
5. Delete: Xóa theo ID
