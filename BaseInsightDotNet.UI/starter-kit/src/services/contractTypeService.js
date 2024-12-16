import axios from "axios";

const CONTROLLER_NAME = "ContractType";

const getAllContractType = async (param) => {
  try {
    const result = await axios.get(
      `https://localhost:7130/api/${CONTROLLER_NAME}/GetAllContractTypes`,
      {
        params: {
          name: param.name,
        },
      }
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};

const createContractType = async (params) => {
  try {
    const result = await axios.post(
      `https://localhost:7130/api/${CONTROLLER_NAME}/CreateContractType`,
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

const updateContractType = async (params) => {
  try {
    const result = await axios.put(
      `https://localhost:7130/api/${CONTROLLER_NAME}/UpdateContractType`,
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

const deleteContractType = async (id) => {
  try {
    const result = await axios.delete(
      `https://localhost:7130/api/${CONTROLLER_NAME}/DeleteContractType/${id}`,
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
const getContractTypeById = async (id) => {
  try {
    const result = await axios.get(
      `https://localhost:7130/api/${CONTROLLER_NAME}/GetContractTypeById/${id}`
    );
    return result.data;
  } catch (error) {
    throw error;
  }
};

export const ContractTypeService = {
  getAllContractType,
  createContractType,
  updateContractType,
  deleteContractType,
  getContractTypeById,
};
