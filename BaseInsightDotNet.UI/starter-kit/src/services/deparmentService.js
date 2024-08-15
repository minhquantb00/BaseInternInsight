import { AuthMessage } from "@/constants/enums";
import axios from "axios";

const CONTROLLER_NAME = "Department";

const errorList = {
  [AuthMessage.ErrorEmailNotActivated]: {
    error: { detail: AuthMessage.EmailNotActivated },
  },
  [AuthMessage.ErrorEmailNotFound]: {
    error: { detail: AuthMessage.LoginError },
  },
  [AuthMessage.ErrorPasswordInvalid]: {
    error: { detail: AuthMessage.LoginError },
  },
  [AuthMessage.ErrorEmailExist]: { error: { detail: AuthMessage.ExistUser } },
  [AuthMessage.ErrorAccountVerified]: {
    error: { detail: AuthMessage.AccountVerified },
  },
};

const getAllDepartments = async (param) => {
  try {
    const result = await axios.get(
      `https://localhost:7130/api/${CONTROLLER_NAME}/GetAllDepartments`, {
        params: {
          name: param.name,
          managerId: param.managerId
        }
      }
    );
    return result.data;
  } catch (error) {
    if (error.response && error.response.data && error.response.data.detail) {
      return errorList[error.response.data.detail];
    } else {
      return { error: AuthMessage.LoginFail };
    }
  }
};


const createDepartment = async (params) => {
  try{
    const result = await axios.post(`https://localhost:7130/api/${CONTROLLER_NAME}/CreateDepartment`, params, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`
      }
    })
    return result.data
  }catch(error){
    if (error.response && error.response.data && error.response.data.detail) {
      return errorList[error.response.data.detail];
    } else {
      return { error: AuthMessage.LoginFail };
    }
  }
}

const updateDepartment = async (params) => {
  try{
    const result = await axios.put(`https://localhost:7130/api/${CONTROLLER_NAME}/UpdateDepartment`, params, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`
      }
    })
    return result.data
  }catch(error){
    if (error.response && error.response.data && error.response.data.detail) {
      return errorList[error.response.data.detail];
    } else {
      return { error: AuthMessage.LoginFail };
    }
  }
}

const deleteDepartment = async (id) => {
  try{
    const result = await axios.delete(`https://localhost:7130/api/${CONTROLLER_NAME}/DeleteDepartment/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`
      }
    })
    return result.data
  }catch(error){
    if (error.response && error.response.data && error.response.data.detail) {
      return errorList[error.response.data.detail];
    } else {
      return { error: AuthMessage.LoginFail };
    }
  }
}
const getDepartmentById = async (id) => {
  try{
    const result = await axios.get(`https://localhost:7130/api/${CONTROLLER_NAME}/GetDepartmentById/${id}`, {
      headers: {
        Authorization: `Bearer ${localStorage.getItem("accessToken")}`
      }
    })
    return result.data
  }catch(error){
    if (error.response && error.response.data && error.response.data.detail) {
      return errorList[error.response.data.detail];
    } else {
      return { error: AuthMessage.LoginFail };
    }
  }
}




export const DeparmentService = {
  getAllDepartments,
  createDepartment,
  updateDepartment,
  deleteDepartment,
  getDepartmentById
}
