import axios from "axios";

const CONTROLLER_NAME = "User";


const getAllUsers = async (param) => {
  try {
    const result = await axios.get(`https://localhost:7130/api/${CONTROLLER_NAME}/GetAllUsers`,
      {
        params: {
          keyWord: param.keyWord,
          departmentId: param.departmentId,
          positionId: param.positionId
        }
      }
    );
    return result.data;
  } catch (error) {
    throw error
  }
};



export const UserService = {
  getAllUsers
}
