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



export const DeparmentService = {
  getAllDepartments
}
