package com.skyline.sdc_project.repository;

import com.skyline.sdc_project.entity.Player;
import org.springframework.data.jpa.repository.JpaRepository;

public interface PlayerRepository extends JpaRepository<Player, Integer> {
}
