import {
  DatePicker,
  Input,
  Select,
  message,
  Modal,
  Button,
  notification,
} from "antd";
import React, { useEffect, useState } from "react";
import {
  createDocument,
  deleteDocument,
  fetchDocuments,
  searchDocument,
  updateDocument,
} from "../api/documentApi";
import DocumentForm from "../components/DocumentForm";
import DocumentList from "../components/DocumentList";
import "./styles.css";
import dayjs from "dayjs";
import { toast, ToastContainer } from "react-toastify";

export default function DocumentPage() {
  const [documents, setDocuments] = useState([]);
  const [filters, setFilters] = useState({
    documentNumber: "",
    documentType: "",
    documentDate: null,
  });

  const [editing, setEditing] = useState<any>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);

  const openNotification = (type: string, content: string) => {
    if (type === "Error") {
      toast.error(content, {
        position: "bottom-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: false,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
      });
    } else if (type === "Success") {
      toast.success(content, {
        position: "bottom-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: false,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
      });
    } else {
      toast(content, {
        position: "bottom-right",
        autoClose: 3000,
        hideProgressBar: false,
        closeOnClick: false,
        pauseOnHover: true,
        draggable: true,
        progress: undefined,
        theme: "light",
      });
    }
  };

  const load = async () => {
    const res = await fetchDocuments();
    setDocuments(res.data);
  };

  useEffect(() => {
    load();
  }, []);

  const handleDelete = async (id: string) => {
    await deleteDocument(id);
    openNotification("", "Đã xóa chứng từ");
    load();
  };

  const handleEdit = (doc: any) => {
    setEditing({
      ...doc,
      documentDate: dayjs(doc.documentDate),
    });
    setIsModalOpen(true);
  };

  const handleCreateClick = () => {
    setEditing(null);
    setIsModalOpen(true);
  };

  const handleSubmit = async (formData: any) => {
    const total = formData.details.reduce(
      (sum: number, d: any) => sum + d.amount,
      0
    );
    if (formData.totalAmount !== total) {
      openNotification("Error", "Tổng tiền không khớp chi tiết!");
      return;
    }

    if (editing) {
      await updateDocument(formData.documentNumber, formData);
      openNotification("Success", "Cập nhật thành công");
    } else {
      await createDocument(formData);
      openNotification("Success", "Thêm chứng từ thành công");
    }

    setIsModalOpen(false);
    setEditing(null);
    load();
  };

  const [debouncedFilters, setDebouncedFilters] = useState(filters);

  useEffect(() => {
    const timeout = setTimeout(() => {
      setDebouncedFilters(filters);
    }, 1000);
    return () => clearTimeout(timeout);
  }, [filters]);

  useEffect(() => {
    fetchFilteredDocuments(debouncedFilters);
  }, [debouncedFilters]);

  const handleSearchChange = (key: string, value: any) => {
    setFilters((prev) => ({ ...prev, [key]: value }));
  };

  const fetchFilteredDocuments = async (filterValues: any) => {
    const res = await searchDocument({
      documentNumber: filterValues.documentNumber,
      documentType: filterValues.documentType,
      documentDate: filterValues.documentDate
        ? filterValues.documentDate.format("YYYY-MM-DD")
        : null,
    });
    setDocuments(res.data);
  };

  return (
    <div className="body">
      <h1 className="header text-xl font-bold mb-4">Quản lý Chứng từ</h1>
      <Button
        type="primary"
        style={{ marginLeft: 16 }}
        onClick={handleCreateClick}
        className="button-add"
      >
        Thêm mới chứng từ
      </Button>
      <div className="mb-4">
        <Input
          placeholder="Tìm theo Số chứng từ"
          value={filters.documentNumber}
          onChange={(e) => handleSearchChange("documentNumber", e.target.value)}
          style={{ width: 200, marginRight: 8 }}
        />
        <Input
          placeholder="Tìm theo Loại chứng từ"
          value={filters.documentType}
          onChange={(e) => handleSearchChange("documentType", e.target.value)}
          style={{ width: 200, marginRight: 8 }}
        />
        <DatePicker
          placeholder="Tìm theo Ngày"
          value={filters.documentDate}
          onChange={(date) => handleSearchChange("documentDate", date)}
        />
      </div>
      <br />
      <DocumentList
        data={documents}
        onDelete={handleDelete}
        onEdit={handleEdit}
      />
      <Modal
        title={editing ? "Chỉnh sửa Chứng từ" : "Thêm mới Chứng từ"}
        open={isModalOpen}
        onCancel={() => setIsModalOpen(false)}
        footer={null}
        destroyOnClose
      >
        <DocumentForm initialValues={editing} onSubmit={handleSubmit} />
      </Modal>
      <ToastContainer
        position="bottom-right"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop={false}
        closeOnClick={false}
        rtl={false}
        pauseOnFocusLoss
        draggable
        pauseOnHover
        theme="light"
      />
    </div>
  );
}
