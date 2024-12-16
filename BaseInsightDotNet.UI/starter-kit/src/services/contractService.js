import axios from "axios";

const CONTROLLER_NAME = "Contract";

const getAllContract = async (param) => {
  try {
    const result = await axios.get(
      `https://localhost:7130/api/${CONTROLLER_NAME}/GetAllContracts`,
      {
        params: {
          contractTypeId: param.contractTypeId,
          employeeId: param.employeeId,
          contractStatus: param.contractStatus,
          fromDate: param.fromDate,
          toDate: param.toDate
        },
      }
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};

const createContract = async (params) => {
  try {
    const result = await axios.post(
      `https://localhost:7130/api/${CONTROLLER_NAME}/CreateContract`,
      params,
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
      }
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};

const updateContract = async (params) => {
  try {
    const result = await axios.put(
      `https://localhost:7130/api/${CONTROLLER_NAME}/UpdateContract`,
      params,
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
      }
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};

const deleteContract = async (id) => {
  try {
    const result = await axios.delete(
      `https://localhost:7130/api/${CONTROLLER_NAME}/DeleteContract/${id}`,
      {
        headers: {
          Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        },
      }
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};
const getContractById = async (id) => {
  try {
    const result = await axios.get(
      `https://localhost:7130/api/${CONTROLLER_NAME}/GetContractById/${id}`
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};

const uploadPhotoContract = async (id, files) => {
  try {
    // const formData = new FormData();
    // files.forEach(file => {
    //   formData.append('files', file);
    // });

    const result = await axios.post(`https://localhost:7130/api/${CONTROLLER_NAME}/UploadPhotoContract/${id}`, files, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`,
        "Content-Type": "multipart/form-data",
      },
    });

    return result.data;
  } catch (error) {
    throw error;
  }
}

export const ContractService = {
  getAllContract,
  createContract,
  updateContract,
  deleteContract,
  getContractById,
  uploadPhotoContract
};
