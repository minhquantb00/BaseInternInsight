import axios from "axios";

const CONTROLLER_NAME = "Position";


const getAllPositions = async (param) => {
  try {
    const result = await axios.get(`https://localhost:7130/api/${CONTROLLER_NAME}/GetAllPositions`);
    return result.data;
  } catch (error) {
    throw error
  }
};



export const PositionService = {
  getAllPositions
}
