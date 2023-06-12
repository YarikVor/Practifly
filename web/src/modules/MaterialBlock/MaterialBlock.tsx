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
                <ListItem className={classes.materialLabel}>
                    <Typography className={classes.materialNameLabel}>Назва</Typography>
                    <Typography className={classes.materialLabel}>Enabled</Typography>
                    <Typography className={classes.materialLabel}>Practical</Typography>
                    <Typography className={classes.materialLabel}>Пріорітетність</Typography>
                </ListItem>
                {materials.map((material) => (
                    <ListItem
                        key={material.id}
                        className={`${classes.materialItem} ${
                            selectedMaterial?.id === material.id ? classes.selectedMaterial : ""
                        }`}
                        onClick={() => handleMaterialClick(material)}
                    >
                        <Box className={classes.materialInfo}>
                            <Typography>{material.name}</Typography>
                            <div className={classes.materialCheckboxRow}>
                                <Checkbox
                                    checked={material.enabled}
                                    onChange={() => handleEnabledChange(material)}
                                    disabled={material.isReserved}
                                />
                            </div>
                            <div className={classes.materialCheckboxRow}>
                                <Checkbox
                                    checked={material.isPractical}
                                    onChange={() => handlePracticalChange(material)}
                                    disabled={material.isReserved}
                                />
                            </div>
                        </Box>
                        <div className={classes.priorityBlock}>
                            <span>{material.priority}</span>
                        </div>
                    </ListItem>
                ))}
            </List>
        </Box>


    );
};

export default MaterialMenu;
