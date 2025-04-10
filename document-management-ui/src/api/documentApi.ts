import axios from 'axios';

const API_BASE_URL = "https://localhost:7227/api";

export const fetchDocuments = () => axios.get(`${API_BASE_URL}/Document`);
export const searchDocument = (data: any) => axios.get(`${API_BASE_URL}/Document`, {
    params: {
        ...data
    }
});
export const createDocument = (data: any) => axios.post(`${API_BASE_URL}/Document`, data);
export const updateDocument = (documentNumber: string, data: any) => {
    return axios.put(`${API_BASE_URL}/Document/${documentNumber}`, data);
};
export const deleteDocument = (id: string) => axios.delete(`${API_BASE_URL}/Document/${id}`);