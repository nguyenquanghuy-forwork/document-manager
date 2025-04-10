import { Button, Popconfirm, Table } from "antd";
import dayjs from "dayjs";

export default function DocumentList({ data, onDelete, onEdit }: any) {
  const columns = [
    { title: "Số chứng từ", dataIndex: "documentNumber" },
    {
      title: "Ngày",
      dataIndex: "documentDate",
      render: (value: string) => dayjs(value).format("DD/MM/YYYY"),
    },
    { title: "Loại", dataIndex: "documentType" },
    { title: "Tổng tiền", dataIndex: "totalAmount" },
    {
      title: "Hành động",
      render: (_: any, record: any) => (
        <>
          <Button
            type="link"
            onClick={() => onEdit(record)}
            style={{ paddingRight: 8 }}
          >
            Sửa
          </Button>
          <Popconfirm
            title="Xóa chứng từ?"
            onConfirm={() => onDelete(record.documentNumber)}
          >
            <Button danger>Xóa</Button>
          </Popconfirm>
        </>
      ),
    },
  ];

  return (
    <Table
      rowKey="documentNumber"
      columns={columns}
      dataSource={data}
      expandable={{
        expandedRowRender: (record) => (
          <div>
            <strong>Chi tiết chứng từ:</strong>
            <ul>
              {record.details.map((d: any, index: number) => (
                <li key={index}>
                  Mã tài khoản: {d.accountCode} - Diễn giải: {d.description} -
                  Số tiền: {d.amount} - Loại giao dịch: {d.transactionType}
                </li>
              ))}
            </ul>
            <p>
              <strong>Tổng chi tiết:</strong>{" "}
              {record.details.reduce(
                (sum: number, d: any) => sum + d.amount,
                0
              )}
            </p>
          </div>
        ),
      }}
    />
  );
}
