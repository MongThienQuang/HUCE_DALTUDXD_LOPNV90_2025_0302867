using Autodesk.Revit.UI; // cho phép pluggin làm việc với giao diện revit 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.DB.Structure;
using System.Xml.Linq;
using System.Xml;
using Autodesk.Revit.DB; // cho phép pluggin làm việc với các đối tượng hình học trong revit 
using System.IO;
using System.Diagnostics;
using System.Data;
using static Autodesk.Revit.DB.SpecTypeId;

namespace DALTUDXD_LOPNV90_2025_0302867

{// khai báo lớp chính pluggin 
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)] //chỉ định revit sẽ ko tự khởi chạy mà bạn phải tự xử lí 
    public class Class1 : IExternalCommand //Giao diện bắt buộc cho bất kỳ lệnh nào gọi từ Revit Add-in.
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements) //Revit sẽ gọi hàm này khi bạn chạy Add-in. Đây là trung tâm xử lý toàn bộ logic của plugin.
        {
            // lấy document hiện tại đăng mở trong revit 
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            ICollection<ElementId> ids = uidoc.Selection.GetElementIds(); //lấy danh sách id của các phần tử người dùng đã chọn 
            // Kiểm tra chọn sàn
            if (ids.Count == 0) //nếu không có đối tượng nào được chọn 
            {
                TaskDialog.Show("Thông báo", "Vui lòng chọn ít nhất một sàn.");
                return Result.Cancelled;
            }

            // Duyệt qua các phần tử được chọn xem phần tử nào là sàn 
            foreach (ElementId elemId in ids)
            {
                Element elem = doc.GetElement(elemId);
                Floor floor = elem as Floor;
                if (floor == null) continue; //nếu có sàn được chọn mới xử lí tiếp 

                // Lấy thông tin sàn
                string level = findLevel(floor, doc);
                double area = toSquareMeters(findArea(floor));
                double thickness = toMeters(findThickness(floor));
                double chieuDai = GetFloorLength(floor);
                double chieuRong = GetFloorWidth(floor);

                // Thêm dữ liệu vào DataGridView của Form1
            }

            // Hiển thị Form
            return Result.Succeeded;
        }

        // CÁC HÀM PHỤ TRỢ 
        private string findLevel(Element elem, Document doc)
        {
            try // bọc đoạn code có nguy cơ bị lỗi, bắt và xử lí lỗi mà ko làm crash chương trình đang chạy 
            {
                Parameter param = elem.LookupParameter("Level"); //tìm tham số tên là "level"
                if (param != null && param.HasValue) // nếu tham số sàn tồn tại và có giá trị 
                {
                    // lấy các thuộc tính của sàn đó: dài, rộng, dày,...) 
                    ElementId id = param.AsElementId();
                    Element level = doc.GetElement(id);
                    return level?.Name ?? "Không rõ";
                }
            }
            catch { } //nếu ko chọn sàn nào: ko xác định được level (bắt lỗi) =>  trả về kết quả "không rõ" 
            return "Không rõ";
        }
        private double findArea(Element elem)
        {
            return elem.LookupParameter("Area").AsDouble();
        }

        private double findThickness(Element elem)
        {
            return elem.LookupParameter("Thickness").AsDouble();
        }

        // chuyển đổi đơn vị sang m2 và m 
        private double toSquareMeters(double feet2)
        {
            return UnitUtils.ConvertFromInternalUnits(feet2, UnitTypeId.SquareMeters);
        }
        private double toMeters(double feet)
        {
            return UnitUtils.ConvertFromInternalUnits(feet, UnitTypeId.Meters);
        }

        //lấy kích thước sàn theo trục x, y sau đó chuyển đơn vị sang m
        private double GetFloorLength(Floor floor)
        {
            BoundingBoxXYZ boundingBox = floor.get_BoundingBox(null);
            return UnitUtils.ConvertFromInternalUnits(boundingBox.Max.X - boundingBox.Min.X, UnitTypeId.Meters);
        }
        private double GetFloorWidth(Floor floor)
        {
            BoundingBoxXYZ boundingBox = floor.get_BoundingBox(null);
            return UnitUtils.ConvertFromInternalUnits(boundingBox.Max.Y - boundingBox.Min.Y, UnitTypeId.Meters);
        }
    }
}
