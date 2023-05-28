import React, { useState } from "react";
import { Box, Typography, List, ListItem, Checkbox } from "@mui/material";

import { useStyles } from "./styles";

interface Material {
    id: number;
    name: string;
    enabled: boolean;
    isPractical: boolean;
    isReserved: boolean;
    priority: number;
}

interface MaterialMenuProps {
    materials: Material[];
}

const MaterialMenu: React.FC<MaterialMenuProps> = ({ materials }) => {
    const classes = useStyles();
    const [selectedMaterial, setSelectedMaterial] = useState<Material | null>(null);

    const handleMaterialClick = (material: Material) => {
        setSelectedMaterial(material);
    };

    const handleEnabledChange = (material: Material) => {
        material.enabled = !material.enabled;
        setSelectedMaterial({ ...material });
    };

    const handlePracticalChange = (material: Material) => {
        material.isPractical = !material.isPractical;
        setSelectedMaterial({ ...material });
    };

    return (
        <Box className={classes.container}>
            <Box className={classes.headerWrapper}>
                <Typography className={classes.headerText}>Меню матеріалів</Typography>
            </Box>
            <List>
                {materials.map((material) => (
                    <ListItem
                        key={material.id}
                        className={`${classes.materialItem} ${
                            selectedMaterial?.id === material.id ? classes.selectedMaterial : ""
                        }`}
                        onClick={() => handleMaterialClick(material)}
                    >
                        <Checkbox
                            checked={material.enabled}
                            onChange={() => handleEnabledChange(material)}
                            disabled={material.isReserved}
                        />
                        <Checkbox
                            checked={material.isPractical}
                            onChange={() => handlePracticalChange(material)}
                            disabled={material.isReserved}
                        />
                        <span>{material.name}</span>
                        <input type="number" value={material.priority} disabled={material.isReserved} />
                    </ListItem>
                ))}
            </List>
        </Box>
    );
};

export default MaterialMenu;
