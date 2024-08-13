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

const login = async (data) => {
  try{
    const result = await axiosIns.post(`${CONTROLLER_NAME}/Login`, data)
    return result.data
  }
  catch(error){
    return errorList[error.response.data.detail] || { error: AuthMessage.LoginFail };
  }
}

const register = async (data) => {
  try{
    const result = await axiosIns.post(`${CONTROLLER_NAME}/RegisterUser`, data)
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

