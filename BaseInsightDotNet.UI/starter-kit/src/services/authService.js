import { AuthMessage } from '@/constants/enums';
import axios from 'axios';

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
      const result = await axios.post(`https://localhost:7130/api/${CONTROLLER_NAME}/Login`, params);
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
    const result = await axios.post(`https://localhost:7130/api/${CONTROLLER_NAME}/RegisterUser`, params, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    })
    return result.data
  }
  catch(error){
    if (error.response && error.response.data && error.response.data.detail) {
      return errorList[error.response.data.detail];
  } else {
      return { error: AuthMessage.LoginFail };
  }
  }
}


export const AuthService = {
  login,
  register
}

