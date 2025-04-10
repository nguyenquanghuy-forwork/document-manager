import { MinusCircleOutlined, PlusOutlined } from "@ant-design/icons";
import {
  Button,
  DatePicker,
  Divider,
  Form,
  Input,
  InputNumber,
  Select,
  Space,
} from "antd";
import dayjs from "dayjs";
import { useEffect } from "react";

const { Option } = Select;

export default function DocumentForm({
  onSubmit,
  initialValues,
}: {
  onSubmit: (values: any) => void;
  initialValues?: any;
}) {
  const [form] = Form.useForm();

  useEffect(() => {
    if (initialValues) {
      form.setFieldsValue(initialValues);
    }
  }, [initialValues]);

  return (
    <Form
      layout="vertical"
      onFinish={(values) => {
        const formatted = {
          ...values,
          documentDate: dayjs(values.documentDate).format("YYYY-MM-DD"),
        };
        onSubmit(formatted);
      }}
      initialValues={{ details: [{}] }}
      form={form}
    >
      <Form.Item
        name="documentNumber"
        label="Số chứng từ"
        rules={[{ required: true }]}
      >
        <Input disabled={!!initialValues} />
      </Form.Item>
      <Form.Item
        name="documentDate"
        label="Ngày chứng từ"
        rules={[{ required: true }]}
      >
        <DatePicker format="YYYY-MM-DD" style={{ width: "100%" }} />
      </Form.Item>
      <Form.Item
        name="documentType"
        label="Loại chứng từ"
        rules={[{ required: true }]}
      >
        <Input />
      </Form.Item>
      <Form.Item name="description" label="Diễn giải">
        <Input />
      </Form.Item>
      <Form.Item
        name="totalAmount"
        label="Tổng tiền"
        rules={[{ required: true }]}
      >
        <InputNumber min={0} style={{ width: "100%" }} />
      </Form.Item>

      <Divider>Chi tiết chứng từ</Divider>
      <Form.List name="details">
        {(fields, { add, remove }) => (
          <>
            {fields.map(({ key, name }) => (
              <Space key={key} align="baseline" style={{ width: "100%" }}>
                <Form.Item
                  name={[name, "accountCode"]}
                  rules={[{ required: true }]}
                >
                  <Input placeholder="Mã tài khoản" />
                </Form.Item>
                <Form.Item name={[name, "description"]}>
                  <Input placeholder="Diễn giải" />
                </Form.Item>
                <Form.Item
                  name={[name, "amount"]}
                  rules={[{ required: true, type: "number", min: 0 }]}
                >
                  <InputNumber placeholder="Số tiền" />
                </Form.Item>
                <Form.Item
                  name={[name, "transactionType"]}
                  rules={[{ required: true }]}
                >
                  <Select placeholder="Loại giao dịch">
                    <Option value="thu">Thu</Option>
                    <Option value="chi">Chi</Option>
                  </Select>
                </Form.Item>
                <MinusCircleOutlined onClick={() => remove(name)} />
              </Space>
            ))}
            <Form.Item>
              <Button
                type="dashed"
                onClick={() => add()}
                icon={<PlusOutlined />}
              >
                Thêm dòng
              </Button>
            </Form.Item>
          </>
        )}
      </Form.List>

      <Form.Item>
        <Button type="primary" htmlType="submit">
          Lưu chứng từ
        </Button>
      </Form.Item>
    </Form>
  );
}
