import {Box} from "@mui/material";

import {MyInput} from "../../UIComponents/Input/MyInput";
import {useAppSelector} from "../../hooks/hooks";

export const MaterialDetail = () => {
  const currentMaterials = useAppSelector((state) => state.course.currentMaterials );
  console.log(currentMaterials);
  return (
    <Box>
      <MyInput value={currentMaterials.data?.name} name="materialName" />
      <MyInput value={currentMaterials.data?.note} name="materialDescription" />
    </Box>
  );
};
