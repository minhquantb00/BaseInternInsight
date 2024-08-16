import axios from "axios";

const CONTROLLER_NAME = "Department";


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
    throw error;
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
    throw error;
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
    throw error;
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
    throw error;
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
    throw error;
  }
}




export const DeparmentService = {
  getAllDepartments,
  createDepartment,
  updateDepartment,
  deleteDepartment,
  getDepartmentById
}
