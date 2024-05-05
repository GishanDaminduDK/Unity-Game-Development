package com.skyline.sdc_project.repository;
import com.skyline.sdc_project.entity.Player;
import org.springframework.data.repository.CrudRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface PlayerRepo extends CrudRepository<Player, Integer> {
    public Player findAdminByUsername(String username);
    public Player findAdminByUsernameAndPassword(String username, String password);

}
