import { AuthMessage } from '@/constants/enums';
import axiosIns from "@/plugins/axios";

const CONTROLLER_NAME = 'Auth'

const errorList = {
  [AuthMessage.ErrorEmailNotActivated]: { error: { detail: AuthMessage.EmailNotActivated } },
  [AuthMessage.ErrorEmailNotFound]: { error: { detail: AuthMessage.LoginError } },
  [AuthMessage.ErrorPasswordInvalid]: { error: { detail: AuthMessage.LoginError } },
  [AuthMessage.ErrorEmailExist]: { error: { detail: AuthMessage.ExistUser } },
  [AuthMessage.ErrorAccountVerified]: { error: { detail: AuthMessage.AccountVerified } },
}

const login = async (params) => {
  try {
    console.log(params)
      const result = await axiosIns.post(`${CONTROLLER_NAME}/Login`, params);
      console.log(result);
      return result.data;
  } catch (error) {
      if (error.response && error.response.data && error.response.data.detail) {
          return errorList[error.response.data.detail];
      } else {
          return { error: AuthMessage.LoginFail };
      }
  }
};


const register = async (params) => {
  try{
    const result = await axiosIns.post(`${CONTROLLER_NAME}/RegisterUser`, params)
    return result.data
  }
  catch(error){
    return {error: AuthMessage.RegisterFail}
  }
}


export const AuthService = {
  login,
  register
}

